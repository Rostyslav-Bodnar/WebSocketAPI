using Microsoft.AspNetCore.Mvc;
using WebSocket_API.DTO;
using WebSocket_API.Interfaces;

namespace WebSocket_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService assetService;
        private readonly ILogger<AssetController> logger;

        public AssetController(IAssetService assetService, ILogger<AssetController> logger)
        {
            this.assetService = assetService;
            this.logger = logger;
        }

        [HttpGet("supported")]
        public async Task<ActionResult<List<SupportedAssetsDTO>>> GetSupportedAssets()
        {
            try
            {
                logger.LogInformation("Fetching supported assets");
                var assets = await assetService.GetSupportedAssetsAsync();
                return Ok(assets);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to fetch supported assets");
                return StatusCode(500, "An error occurred while fetching supported assets.");
            }
        }

        [HttpGet("{symbol}")]
        public async Task<ActionResult<List<AssetDTO>>> GetAssetBySymbol([FromQuery] string symbol, [FromQuery] string? providerName = null)
        {
            try
            {
                logger.LogInformation("Fetching asset by symbol: {Symbol}, provider: {ProviderName}", symbol, providerName ?? "none");
                var assets = await assetService.GetAssetBySymbolAsync(symbol, providerName);
                if (!assets.Any())
                {
                    logger.LogWarning("No assets found for symbol: {Symbol}, provider: {ProviderName}", symbol, providerName ?? "none");
                    return NotFound($"No assets found for symbol: {symbol}");
                }
                return Ok(assets);
            }
            catch (ArgumentException ex)
            {
                logger.LogError(ex, "Invalid input for symbol: {Symbol}, provider: {ProviderName}", symbol, providerName ?? "none");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to fetch asset by symbol: {Symbol}", symbol);
                return StatusCode(500, "An error occurred while fetching asset data.");
            }
        }

        [HttpGet("prices")]
        public async Task<ActionResult<List<AssetDTO>>> GetAssetsPrices([FromQuery] int[] assetIds)
        {
            try
            {
                logger.LogInformation("Fetching prices for assets with IDs: {AssetIds}", string.Join(", ", assetIds));
                var prices = await assetService.GetAssetsPricesAsync(assetIds);
                if (!prices.Any())
                {
                    logger.LogWarning("No prices found for asset IDs: {AssetIds}", string.Join(", ", assetIds));
                    return NotFound("No prices found for the provided asset IDs.");
                }
                return Ok(prices);
            }
            catch (ArgumentException ex)
            {
                logger.LogError(ex, "Invalid input for asset IDs: {AssetIds}", string.Join(", ", assetIds));
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to fetch prices for assets");
                return StatusCode(500, "An error occurred while fetching asset prices.");
            }
        }

        [HttpDelete("symbol")]
        public async Task<ActionResult> DeleteAsset([FromQuery] string symbol)
        {
            var result = await assetService.DeleteAssetAsync(symbol);

            if (result == null) return BadRequest();

            return Ok(result);
        }

        [HttpGet("by-symbols")]
        public async Task<ActionResult<List<AssetDTO>>> GetAssetsBySymbols(
            [FromQuery] string[] symbols,
            [FromQuery] string? providerName = null)
        {
            try
            {
                var result = await assetService.GetAssetsBySymbolsAsync(symbols, providerName);

                if (!result.Any())
                {
                    logger.LogWarning("No assets found for provided symbols");
                    return NotFound("No assets found for the provided symbols.");
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                logger.LogError(ex, "Invalid input for symbols");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to fetch assets for symbols");
                return StatusCode(500, "An error occurred while fetching asset data.");
            }
        }

    }
}
